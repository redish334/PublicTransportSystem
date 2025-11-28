using System.Collections.Generic;
public interface IDriverRepository
{
    IEnumerable<Driver> GetAll();
    void Add(string first, string last, string phone, string lic);
    void SoftDelete(int id);
}
