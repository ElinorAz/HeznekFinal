import { ResidenceEnum } from 'src/app/enums/residence.enum';

export interface AcademicStudiesModel
{
    academicDegree: string;
    fieldOfStudy: string;
    academicInstitution: string;
    residence: ResidenceEnum;
    graduationYear : number;

    gradesFileName: string;
    grades: File;
    aprovalDownloadName?: string;
    gradesDownloadName?: string;
    aprovalFileName:string;
    aproval:File;
}