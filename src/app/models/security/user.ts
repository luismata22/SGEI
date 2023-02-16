import { RolesxUser } from "./rolesxuser";

export class User{
    id: number = -1;
    nombres: string = "";
    apellidos: string = "";
    correo: string = "";
    cedula: string = "";
    clave: string = "";
    activo: boolean = false;
    rolesxusuario: RolesxUser[] = [];
}