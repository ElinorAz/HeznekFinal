import { ParentsSalaryModel } from './parents-salary.model';
import { TaskModel } from './task.model';
import { FormStatusEnum } from '../../enums/form-status.enum';

export interface FormModel
{
    parentsSalary: ParentsSalaryModel;
    status: FormStatusEnum
    tasks: TaskModel[];
}