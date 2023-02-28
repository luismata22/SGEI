import { PermissionxModule } from "./permissionxmodule";
import { Role } from "./role";

export class PermissionxModulexRole{
    id: number = -1;
    idpermisoxmodulo: number = -1;
    idrol: number = -1;
    activo: boolean = false;
    rol: Role = new Role();
    permisosxmodulo: PermissionxModule = new PermissionxModule();
}