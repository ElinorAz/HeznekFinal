import { GenderEnum } from 'src/app/enums/gender.enum';
import { MilitaryServiceModel } from './military-service.model';
import { HighSchoolModel } from './high-school.model';
import { generate } from 'rxjs';
import { UserGeneralInfoModel } from './user-general-info.model';
import { CandidateAdditionalDataModel } from './candidate-additional-data.model';
import { AcademicStudiesModel } from './academic-stadies.model';
import { VolunteerHourModel } from '../../models/volunteer-hours/volunteer-hours.model';
import { VolunteerDetailsModel } from '../../models/volunteer-hours/volunteer-details.model';

export interface CandidateProfileModel{
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
    volunteerHoursA?: VolunteerHourModel[];
    volunteerHoursB?: VolunteerHourModel[];
    volunteerDetails?: VolunteerDetailsModel;
}