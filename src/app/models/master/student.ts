import { Person } from "./person";
import { PersonxStudent } from "./personsxstudent";

export class Student{
    id: number = -1;
    nombres: string = "";
    apellidos: string = "";
    idtipocurso: number = -1;
    fechanacimiento: Date;
    personasxestudiante: PersonxStudent[] = [];
}