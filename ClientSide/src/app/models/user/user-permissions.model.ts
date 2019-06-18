import { UserRoleEnum } from 'src/app/enums/user-role.enum';

export interface UserPermissionsModel
{
    role: UserRoleEnum;
    email: string;
    firstName: string;
    lastName: string;
    id: string;
}