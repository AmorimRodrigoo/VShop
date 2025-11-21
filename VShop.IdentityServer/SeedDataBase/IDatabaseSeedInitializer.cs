namespace VShop.IdentityServer.SeedDataBase;

public interface IDatabaseSeedInitializer
{
    void InitializeSeedUser();
    void InitializeSeedRole();
}