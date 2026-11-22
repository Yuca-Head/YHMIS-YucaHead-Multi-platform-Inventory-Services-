namespace YHMIS.Thirdparty.Domain.Roles;

public sealed partial record SupplierRole
{
    private SupplierRole(ThirdpartyRole role, string? ruc)
    {
        Role = role;
        RUC = ruc;
    }

    /*
        
        «A partir del 1 de julio del presente año, los números RUC válidos serán aquellos que constan de -catorce- dígitos.
        Los contribuyentes que aun tengan RUC con diez dígitos, deben dirigirse a su Administración de Rentas para cambiar 
        su número RUC.» (legislacion.asamblea: 2013; Sec: "Dispone", Pto: "Tercero").

        (Su primer carácter es una letra).
        Link: http://legislacion.asamblea.gob.ni/normaweb.nsf/b92aaea87dac762406257265005d21f7/b0b438cc28cbc7f506257b0c0065ce9e
    */
    internal const int RucLenth = 14;
    public ThirdpartyRole Role {get; private init;}
    public string? RUC {get; private init;}
}