import { GenderEnum } from "../../enums/gender.enum";
import { MilitaryServiceModel } from "../../models/user/military-service.model";
import { HighSchoolModel } from "../../models/user/high-school.model";
import { UserGeneralInfoModel } from "../../models/user/user-general-info.model";
import { CandidateAdditionalDataModel } from "../../models/user/candidate-additional-data.model";
import { AcademicStudiesModel } from "../../models/user/academic-stadies.model";
import { ParentsSalaryModel } from "../../models/candidate/parents-salary.model";
import { TaskModel } from "../../models/candidate/task.model";
import { FormModel } from "../../models/candidate/fom.model";
import { TelephonyModel } from "../../models/student/telephony.model";


export interface AdminCandidateProfileModel{
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
    userId?: string;
    id?: number;
    candidateForm: FormModel,
    telephony?: TelephonyModel;
}