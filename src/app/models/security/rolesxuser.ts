import { Role } from "./role";

export class RolesxUser{
    id: number = -1;
    idusuario: number = -1;
    idrol: number = -1;
    activo: boolean = false;
    rol: Role = new Role();
}