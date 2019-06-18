import { HeznekProgramModel } from './heznek-program.model';

export interface UserGeneralInfoModel
{
    psychometricGrade: number;
    worthyOfAdvancment: boolean;
    participationInPrograms: HeznekProgramModel[];
    ease: string;
    points: number;
    disabilities: boolean;
}