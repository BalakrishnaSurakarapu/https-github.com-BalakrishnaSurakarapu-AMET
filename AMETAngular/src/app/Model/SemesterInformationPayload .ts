
export interface SemesterInformationPayload {
  semesterInfoID: number;
  semesterStartDate: string;   // ISO Date string
  semesterEndDate: string;     // ISO Date string
  holiday: string;
  scheduleOrder: string;
  startingDayOrder: string;
  noOfDaysPerWeek: number;
  noOfWorkingDays: number;
  noOfWorkingHours: number;
  fullDayTotalHours: number;
  fullDayMinHours: number;
  firstHalfDayTotalHours: number;
  firstHalfDayMinHours: number;
  secondHalfDayTotalHours: number;
  secondHalfDayMinHours: number;
  collegeID: number;
  batchID: number;
  degreeID: number;
  branchID: number;
  semesterID: number;
  attendanceSettings: AttendanceSettingsPayload;
  gradeSettings: GradeSettingsPayload[];
}

export interface AttendanceSettingsPayload {
  attendanceSettingID: number;
  maxMark: number;
  minPercentToWriteExam: number;
  minPercentToWriteNextYear: number;
  attendanceMarkCriteria: AttendanceMarkCriteriaPayload[];
}

export interface AttendanceMarkCriteriaPayload {
  criteriaID: number;
  fromPercent: number;
  toPercent: number;
  mark: number;
}

export interface GradeSettingsPayload {
  gradeSettingID: number;
  fromMark: number;
  toMark: number;
  markGrade: string;
  point: number;
}


