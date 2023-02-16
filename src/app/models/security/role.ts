import { PermissionxRole } from "./permissionxrole";

export class Role{
    id: number = -1;
    nombre: string = "";
    descripcion: string = "";
    activo: boolean = false;
    permisosxrole: PermissionxRole[] = [];
}