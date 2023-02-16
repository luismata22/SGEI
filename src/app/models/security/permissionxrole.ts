import { Permissions } from "./permissions";

export class PermissionxRole{
    id: number = -1;
    idpermiso: number = -1;
    idrol: number = -1;
    activo: boolean = false;
    permiso: Permissions = new Permissions();
}