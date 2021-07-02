import { Candidate } from "./candidate";

export class ElectionView{

    idElectoralRoom:number;
    electionName:string;
    electionDate:Date=new Date();
    college:string;
    department:string;
    candidates:Candidate[]=[];
}