import { UserRoleEnum } from 'src/app/enums/user-role.enum';
import { UserStatusEnum } from 'src/app/enums/user-status.enum';

export interface TokenModel
{
    sid: string;
    role: UserRoleEnum;
    actor: UserStatusEnum;
}