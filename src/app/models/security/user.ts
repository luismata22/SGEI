import { Person } from "../master/person";
import { RolesxUser } from "./rolesxuser";

export class User{
    id: number = -1;
    clave: string = "";
    activo: boolean = false;
    persona: Person = new Person();
    rolesxusuario: RolesxUser[] = [];
}