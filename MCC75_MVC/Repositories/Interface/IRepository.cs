namespace MCC75_MVC.Repositories.Interface;

interface IRepository<Key, Entity> where Entity: class
{
    List<Entity> GetAll();
    Entity GetById(Key key);
    int Insert(Entity entity);
    int Update(Entity entity);
    int Delete(Key key);
}
