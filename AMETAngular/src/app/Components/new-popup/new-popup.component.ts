import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  OnChanges,
  SimpleChanges,
  Output,
} from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SemesterInformationPayload } from 'src/app/Model/SemesterInformationPayload ';
import { NewService } from 'src/app/Services/new.service';

@Component({
  selector: 'app-new-popup',
  templateUrl: './new-popup.component.html',
  styleUrls: ['./new-popup.component.css'],
})
export class NewPopupComponent implements OnInit, OnChanges {
  @Input() record: any = null;
  @Output() closed = new EventEmitter<boolean>();
  semesterForm!: FormGroup;
  loading = false;

  colleges: any[] = [];
  batches: any[] = [];
  degrees: any[] = [];
  branches: any[] = [];
  semesters: any[] = [];

  weekdays: { id: string; name: string }[] = [
  { id: 'Sunday', name: 'Sunday' },
  { id: 'Monday', name: 'Monday' },
  { id: 'Tuesday', name: 'Tuesday' },
  { id: 'Wednesday', name: 'Wednesday' },
  { id: 'Thursday', name: 'Thursday' },
  { id: 'Friday', name: 'Friday' },
  { id: 'Saturday', name: 'Saturday' }
];
  showDiv1 = false;
  showDiv2 = false;
  showDiv3 = false;

  editingSemesterId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private studentService: NewService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initializeForms();
    this.loadDropdowns();

    if (this.record) {
      this.patchForm(this.record);
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['record'] && changes['record'].currentValue) {
      this.patchForm(changes['record'].currentValue);
    }
  }

  initializeForms(): void {
    this.semesterForm = this.fb.group({
      semesterInfoID: [null],
      college: ['', Validators.required],
      batch: ['', Validators.required],
      degree: ['', Validators.required],
      branch: ['', Validators.required],
      semester: ['', Validators.required],

      // semester info fields (top-level for your template)
      semesterStartDate: ['', Validators.required],
      semesterEndDate: ['', Validators.required],
      holiday: ['', Validators.required],
      scheduleOrder: ['', Validators.required],
      daysPerWeek: ['', Validators.required],
      startingDayOrder: ['', Validators.required],
      workingDays: ['', Validators.required],
      workingHours: ['', Validators.required],

      // day hours (top-level controls as your template expects)
      fullDayHours: ['', Validators.required],
      fullDayMinHours: ['', Validators.required],
      firstHalfHours: ['', Validators.required],
      firstHalfMinHours: ['', Validators.required],
      secondHalfHours: ['', Validators.required],
      secondHalfMinHours: ['', Validators.required],

      // Attendance settings
      maxmarks: ['', Validators.required], // keep if you need max marks
      minEligibilityCurrent: ['', Validators.required],
      minEligibilityNext: ['', Validators.required],
      internalMarks: this.fb.array([this.createInternalMarkGroup()]),

      // Grade settings (moved into main form)
      grades: this.fb.array([this.createGradeRow()]),
    });
  }

  // ---- Internal Marks (Attendance) ----
  createInternalMarkGroup(): FormGroup {
    return this.fb.group({
      from: ['', Validators.required],
      to: ['', Validators.required],
      mark: ['', Validators.required],
    });
  }

  get internalMarks(): FormArray {
    return this.semesterForm.get('internalMarks') as FormArray;
  }

  addInternalMarkRow(): void {
    this.internalMarks.push(this.createInternalMarkGroup());
  }

  removeInternalMarkRow(index: number): void {
    if (this.internalMarks.length > 1) {
      this.internalMarks.removeAt(index);
    }
  }

  // ---- Grades ----
  createGradeRow(): FormGroup {
    return this.fb.group({
      from: ['', Validators.required],
      to: ['', Validators.required],
      grade: ['', Validators.required],
      point: ['', Validators.required],
    });
  }

  get grades(): FormArray {
    return this.semesterForm.get('grades') as FormArray;
  }

  addRow(): void {
    this.grades.push(this.createGradeRow());
  }

  removeRow(index: number): void {
    if (this.grades.length > 1) {
      this.grades.removeAt(index);
    }
  }

  // ---- Dropdowns ----
  loadDropdowns(): void {
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

  // ---- Payload builder (reads arrays from correct path) ----
  buildPayload(): SemesterInformationPayload {
    const f = this.semesterForm.value;

    return {
      semesterInfoID: f.semesterInfoID || 0,
      semesterStartDate: f.semesterStartDate,
      semesterEndDate: f.semesterEndDate,
      holiday: f.holiday,
      scheduleOrder: f.scheduleOrder,
      startingDayOrder: f.startingDayOrder,
      noOfDaysPerWeek: f.daysPerWeek,
      noOfWorkingDays: f.workingDays,
      noOfWorkingHours: f.workingHours,
      fullDayTotalHours: f.fullDayHours,
      fullDayMinHours: f.fullDayMinHours,
      firstHalfDayTotalHours: f.firstHalfHours,
      firstHalfDayMinHours: f.firstHalfMinHours,
      secondHalfDayTotalHours: f.secondHalfHours,
      secondHalfDayMinHours: f.secondHalfMinHours,

      collegeID: +f.college,
      batchID: +f.batch,
      degreeID: +f.degree,
      branchID: +f.branch,
      semesterID: +f.semester,

      attendanceSettings: {
        attendanceSettingID: 0,
        maxMark: f.maxmarks,
        minPercentToWriteExam: f.minEligibilityCurrent,
        minPercentToWriteNextYear: f.minEligibilityNext,
        attendanceMarkCriteria: (f.internalMarks || []).map((c: any) => ({
          criteriaID: 0,
          fromPercent: c.from,
          toPercent: c.to,
          mark: c.mark,
        })),
      },

      gradeSettings: (f.grades || []).map((g: any) => ({
        gradeSettingID: 0,
        fromMark: g.from,
        toMark: g.to,
        markGrade: g.grade,
        point: g.point,
      })),
    } as SemesterInformationPayload;
  }

  toggleDiv(div: number): void {
    this.showDiv1 = div === 1 ? !this.showDiv1 : false;
    this.showDiv2 = div === 2 ? !this.showDiv2 : false;
    this.showDiv3 = div === 3 ? !this.showDiv3 : false;
  }
  // ---- Submit / CRUD ----
  onSubmit(): void {
    if (this.semesterForm.invalid) {
      this.semesterForm.markAllAsTouched();
      return;
    }

    const payload = this.buildPayload();
    console.log('gahg', payload);
    if (this.semesterForm.value.semesterInfoID) {
      this.updateSemester(this.semesterForm.value.semesterInfoID, payload);
    } else {
      this.studentService.createsementicinfo(payload).subscribe({
        next: () => {
          alert('Semester added successfully');
          this.semesterForm.reset();
          this.editingSemesterId = null;
          this.closePopup();
        },
        error: (err) => console.error('Error adding semester', err),
      });
    }
  }

  onUpdate(): void {
    if (!this.editingSemesterId) {
      alert('No semester selected for update');
      return;
    }
    this.updateSemester(this.editingSemesterId, this.buildPayload());
  }

  private updateSemester(
    id: number,
    payload: SemesterInformationPayload
  ): void {
    this.studentService.updatesementicinfo(id, payload).subscribe({
      next: () => {
        alert('Semester updated successfully');
        this.semesterForm.reset();
        this.editingSemesterId = null;
        this.closePopup();
      },
      error: (err) => console.error('Error updating semester', err),
    });
  }

  onDelete(): void {
    if (!this.editingSemesterId) {
      alert('No semester selected for deletion');
      return;
    }

    if (confirm('Are you sure you want to delete this semester?')) {
      this.studentService.deletesementicinfo(this.editingSemesterId).subscribe({
        next: () => {
          alert('Semester deleted successfully');
          this.semesterForm.reset();
          this.editingSemesterId = null;
          this.closePopup();
        },
        error: (err) => console.error('Error deleting semester', err),
      });
    }
  }

  // ---- Patch form for edit ----
  patchForm(payload: any): void {
    if (!payload) return;
    this.editingSemesterId = payload.semesterInfoID;

    this.semesterForm.patchValue({
      semesterInfoID: payload.semesterInfoID,
      college: payload.collegeID,
      batch: payload.batchID,
      degree: payload.degreeID,
      branch: payload.branchID,
      semester: payload.semesterID,
      semesterStartDate: this.formatDate(payload.semesterStartDate),
      semesterEndDate: this.formatDate(payload.semesterEndDate),
      holiday: payload.holiday,
      scheduleOrder: payload.scheduleOrder || '',
      startingDayOrder: payload.startingDayOrder || '',
      daysPerWeek: payload.noOfDaysPerWeek || null,
      workingDays: payload.noOfWorkingDays || null,
      workingHours: payload.noOfWorkingHours || null,
      fullDayHours: payload.fullDayTotalHours || null,
      fullDayMinHours: payload.fullDayMinHours || null,
      firstHalfHours: payload.firstHalfDayTotalHours || null,
      firstHalfMinHours: payload.firstHalfDayMinHours || null,
      secondHalfHours: payload.secondHalfDayTotalHours || null,
      secondHalfMinHours: payload.secondHalfDayMinHours || null,
      maxmarks: payload.attendanceSettings?.maxMark || null,
      minEligibilityCurrent:
        payload.attendanceSettings?.minPercentToWriteExam || null,
      minEligibilityNext:
        payload.attendanceSettings?.minPercentToWriteNextYear || null,
    });

    // ---- Attendance criteria (internalMarks) ----
    this.internalMarks.clear();
    if (payload.attendanceSettings?.attendanceMarkCriteria?.length) {
      payload.attendanceSettings.attendanceMarkCriteria.forEach((crit: any) => {
        this.internalMarks.push(
          this.fb.group({
            from: [crit.fromPercent, Validators.required],
            to: [crit.toPercent, Validators.required],
            mark: [crit.mark, Validators.required],
          })
        );
      });
    } else {
      this.addInternalMarkRow(); // at least 1 row
    }

    // ---- Grades ----
    this.grades.clear();
    if (payload.gradeSettings?.length) {
      payload.gradeSettings.forEach((g: any) => {
        this.grades.push(
          this.fb.group({
            from: [g.fromMark, Validators.required],
            to: [g.toMark, Validators.required],
            grade: [g.markGrade, Validators.required],
            point: [g.point, Validators.required],
          })
        );
      });
    } else {
      this.addRow();
    }
  }

  // ---- Helper: convert API date to yyyy-MM-dd format for input[type=date] ----
  formatDate(dateStr: string | null): string {
    if (!dateStr) return '';
    const d = new Date(dateStr);
    const month = ('0' + (d.getMonth() + 1)).slice(-2);
    const day = ('0' + d.getDate()).slice(-2);
    return `${d.getFullYear()}-${month}-${day}`;
  }

  closePopup(): void {
    this.closed.emit(true);
  }
  //error messages
  isInvalid(controlName: string): boolean {
    const control = this.semesterForm.get(controlName);
    return !!(control && control.invalid && (control.dirty || control.touched));
  }
  isInvalidArrayControl(index: number, controlName: string): boolean {
  const control = this.internalMarks.at(index).get(controlName);
  return !!(control && control.invalid && (control.touched || control.dirty));
}
  get f() {
    return this.semesterForm.controls;
  }

 
}
