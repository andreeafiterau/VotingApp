import { College } from "./college";
import { Department } from "./department";
import { Role } from "./role";
import { User } from "./user";

export class UserAdminView{

    user:User=new User();
    role:Role=new Role();
    colleges:College[]=[];
    departments:Department[]=[];
}