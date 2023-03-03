import { Person } from "./person";
import { Student } from "./student";

export class PersonxStudent{
    id: number = -1;
    idpersona: number = -1;
    idestudiante: number = -1;
    activo: boolean = false;
    persona: Person = new Person();
    estudiante: Student = new Student();
    esrepresentante: boolean = false;
}