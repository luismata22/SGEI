import { Person } from "./person";

export class Student{
    id: number = -1;
    nombres: string = "";
    apellidos: string = "";
    idtipodecurso: number = -1;
    fechanacimiento: Date;
    representantes: Person[] = [];
}