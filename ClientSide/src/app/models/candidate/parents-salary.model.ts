import { ParentEnum } from 'src/app/enums/parent.enum';

export interface ParentsSalaryModel
{
    name: string;
    fatherName: string;
    motherName: string;
    motherDisability: boolean;
    fatherDisability: boolean;
    salarySlipsFileName: string;
    disabilityFileName: string;
    disability: File;
    disability2FileName: string;
    disability2: File;
    salarySlips: File;
    lastUpdated: Date;
}