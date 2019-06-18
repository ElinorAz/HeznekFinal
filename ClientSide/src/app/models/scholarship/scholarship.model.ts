import { StudentModel } from "../../models/student/student.model";

export interface ScholarshipModel
{
    name: string;
    admission: string;
    status: number;
    budget: number;
    id: number;
    students: StudentModel[];
}