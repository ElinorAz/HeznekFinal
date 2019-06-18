import { UserRoleEnum } from 'src/app/enums/user-role.enum';
import { UserStatusEnum } from 'src/app/enums/user-status.enum';

export interface UserModel
{
    userRole: UserRoleEnum;
    userStatus: UserStatusEnum;
    token?: string;
    id: string;
}