import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NewService } from 'src/app/Services/new.service';

@Component({
  selector: 'app-new-data',
  templateUrl: './new-data.component.html',
  styleUrls: ['./new-data.component.css'],
})
export class NewDataComponent implements OnInit {
  semesterForm!: FormGroup;
  showPopup = false;
  editingRecord: any = null;

  colleges: any[] = [];
  batches: any[] = [];
  degrees: any[] = [];
  branches: any[] = [];
  semesters: any[] = [];
  semesterinfo: any[] = [];

allSemesterInfo: any[] = [];
  constructor(private fb: FormBuilder, private studentService: NewService) {}

  ngOnInit(): void {
    this.initForm();
    this.loadDropdownData();
    this.loadSemesterInfo();

  }

  initForm(): void {
    this.semesterForm = this.fb.group({
      college: [''], // Validators.required
      batch: [''],
      degree: [''],
      branch: [''],
      semester: [''],
    });
  }

  // Load dropdowns in parallel
  loadDropdownData(): void {
    this.studentService
      .getCollege()
      .subscribe((data) => (this.colleges = data));
    this.studentService.getBatch().subscribe((data) => (this.batches = data));
    this.studentService.getdegree().subscribe((data) => (this.degrees = data));
    this.studentService.getBranch().subscribe((data) => (this.branches = data));
    this.studentService
      .getsemester()
      .subscribe((data) => (this.semesters = data));
  }
  //------------
  loadSemesterInfo(): void {
  this.studentService.getsementicinfo().subscribe((data: any[]) => {
    this.semesterinfo = data.map((s) => ({
      ...s,
      totalWorkingDaysfrom: this.calculateTotalWorkingDays(
        s.semesterStartDate,
        s.semesterEndDate,
        s.noOfDaysPerWeek
      ),
      remainingWorkingDays: this.calculateRemainingWorkingDays(
        s.semesterStartDate,
        s.semesterEndDate,
        s.noOfDaysPerWeek
      ),
    })); this.allSemesterInfo = [...this.semesterinfo];
  });
}

private calculateTotalWorkingDays(
  semesterStart: string,
  semesterEnd: string,
  workingDaysPerWeek: number
): number {
  const start = new Date(semesterStart);
  const end = new Date(semesterEnd);
  let totalWorkingDays = 0;
  let current = new Date(start);
  while (current <= end) {
    const dayOfWeek = current.getDay();
    if (dayOfWeek !== 0 && dayOfWeek !== 6) {
      totalWorkingDays++;
    }
    current.setDate(current.getDate() + 1);
  }
  return Math.floor((totalWorkingDays / 5) * workingDaysPerWeek);
}

private calculateRemainingWorkingDays(
  semesterStart: string,
  semesterEnd: string,
  workingDaysPerWeek: number
): number {
  const today = new Date();
  const start = new Date(semesterStart);
  const end = new Date(semesterEnd);

  let current = today < start ? start : today;
  let remaining = 0;

  while (current <= end) {
    const dayOfWeek = current.getDay();
    if (dayOfWeek !== 0 && dayOfWeek !== 6) {
      remaining++;
    }
    current.setDate(current.getDate() + 1);
  }

  return Math.floor((remaining / 5) * workingDaysPerWeek);
}

//----------------------
  openPopup(record?: any): void {
    this.editingRecord = record ? { ...record } : null;
    this.showPopup = true;
  }

  onPopupClosed(updated: boolean = false): void {
    this.showPopup = false;
    this.editingRecord = null;
    if (updated) {
      this.loadSemesterInfo();
    }
  }
  onEdit(id: number): void {
    this.studentService.getsementicinfobyid(id).subscribe({
      next: (data) => {
        console.log('Fetched record for edit:', data);
        this.editingRecord = data; // pass full record to popup
        this.showPopup = true;
      },
      error: (err) => {
        console.error('Failed to fetch record by ID:', err);
        alert('Unable to fetch record for editing.');
      },
    });
  }
  onDelete(id: number): void {
    if (confirm('Delete this Record?')) {
      console.log('del', id);
      this.studentService.deletesementicinfo(id).subscribe({
        next: () => {
          alert('Deleted successfully');
          this.loadSemesterInfo();
        },
        error: (err) => {
          console.error('Delete failed:', err);
          alert('Failed to delete record.');
        },
      });
    }
  }

  onSubmit(): void {
  this.applyFilters();
    console.log('Search Filter:', this.semesterForm.value);
  }

  get f() {
    return this.semesterForm.controls;
  }

applyFilters(): void { debugger
  const { college, batch, degree, branch, semester } = this.semesterForm.value;

  // If nothing selected, show all
  if (!college && !batch && !degree && !branch && !semester) {
    this.semesterinfo = [...this.allSemesterInfo];
    return;
  }

  this.semesterinfo = this.allSemesterInfo.filter((rec) =>
   (!college || rec.collegeID == college) &&
    (!batch || rec.batchID == batch) &&    
    (!branch || rec.branchID == branch) &&
    (!semester || rec.semesterID == semester)
  );
}
}
