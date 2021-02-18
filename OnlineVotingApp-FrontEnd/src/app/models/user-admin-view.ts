import { College } from "./college";
import { Department } from "./department";
import { Role } from "./role";
import { User } from "./user";

export class UserAdminView{

    user:User;
    role:Role;
    colleges:College[];
    departments:Department[];
}