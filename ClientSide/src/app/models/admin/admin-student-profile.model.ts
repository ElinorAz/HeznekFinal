import { GenderEnum } from "../../enums/gender.enum";
import { ScholarshipModel } from "../../models/scholarship/scholarship.model";
import { AcademicStudiesModel } from "../../models/user/academic-stadies.model";
import { BankServiceModel } from "../../models/user/bank.model";
import { CandidateAdditionalDataModel } from "../../models/user/candidate-additional-data.model";
import { HighSchoolModel } from "../../models/user/high-school.model";
import { MilitaryServiceModel } from "../../models/user/military-service.model";
import { UserGeneralInfoModel } from "../../models/user/user-general-info.model";
import { VolunteerHourModel } from "../../models/volunteer-hours/volunteer-hours.model";
import { VolunteerDetailsModel } from "../../models/volunteer-hours/volunteer-details.model";
import { StudentScholarshipModel } from "../../models/student/student-scholarship.model";


export interface AdminStudentProfileModel {
    city: string;
    gender: GenderEnum;
    birthDate: Date;
    address: string;
    siblings: number;
    phone: string;
    academicParents: number;
    militaryService: MilitaryServiceModel;
    highSchool: HighSchoolModel;
    general: UserGeneralInfoModel;
    candidateAdditionalData: CandidateAdditionalDataModel;
    academicStudies: AcademicStudiesModel;
    userId: string;
    id: number;
    scholarDetails: ScholarshipModel;
    bankInfo: BankServiceModel;
    volunteerHoursA?: VolunteerHourModel[];
    volunteerHoursB?: VolunteerHourModel[];
    volunteerDetails?: VolunteerDetailsModel;
    scholarships?: StudentScholarshipModel[];
}