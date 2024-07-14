using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DalApi
{
    public interface ICrud<T> //where T: struct
    {
 
        public int Add(T value);
        public void Update(T value);
        public void Delete(int value);
        public T Get(int id);
        
        public IEnumerable<T?> GetAll(Func<T?, bool>? cond = null);
        public T GetElementByCondition(Func<T?, bool>? cond);

    }
}
