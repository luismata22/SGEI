
import { Module } from "./module";
import { Permissions } from "./permissions";

export class PermissionxModule{
    id: number = -1;
    idmodulo: number = -1;
    idpermiso: number = -1;
    activo: boolean = false;
    permiso: Permissions = new Permissions();
    modulo: Module = new Module();
    checked: boolean = false;
}