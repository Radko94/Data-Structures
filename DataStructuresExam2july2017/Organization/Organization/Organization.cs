using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Organization : IOrganization
{
    int keyIndex;
    Dictionary<int, Person> company;

    public Organization()
    {
        keyIndex = 0;
        company = new Dictionary<int, Person>();
    }

    bool isUpdatedArr = false;
    private Person[] updatePersonArr { get; set; }

    public IEnumerator<Person> GetEnumerator()
    {
        foreach (var item in company)
        {
            if (item.Value == null)
                break;
            yield return item.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public int Count => company.Count;
    public bool Contains(Person person)
    {
        foreach (var item in company)
        {
            if (item.Value.Equals(person))
                return true;
        }

        return false;
    }

    public bool ContainsByName(string name)
    {
        foreach (var item in company)
        {
            if (item.Value.Name == name)
                return true;
        }

        return false;
    }

    public void Add(Person person)
    {
        company.Add(keyIndex, person);
        keyIndex++;
        isUpdatedArr = true;
    }

    public Person GetAtIndex(int index)
    {
        if (index > keyIndex)
            throw new IndexOutOfRangeException();

        return company[keyIndex];
    }

    public IEnumerable<Person> GetByName(string name)
    {
        if (isUpdatedArr)
            updatePersonArr = company.Select(x => x.Value).ToArray();

        List<Person> returnThis = new List<Person>();

        for (int i = 0; i < updatePersonArr.Length; i++)
        {
            if (updatePersonArr[i].Name == name)
                returnThis.Add(updatePersonArr[i]);
        }

        return returnThis;
    }

    public IEnumerable<Person> FirstByInsertOrder(int count)
    {
        return company.Select(x => x.Value).Take(count);
    }

    public IEnumerable<Person> FirstByInsertOrder()
    {
        List<Person> kur = new List<Person>();
        if (Count != 0)
            kur.Add(company[1]);
        return kur;
    }


    public IEnumerable<Person> SearchWithNameSize(int minLength, int maxLength)
    {
        List<Person> kur = new List<Person>();

        foreach (var item in company)
        {
            if (item.Value.Name.Length >= minLength && item.Value.Name.Length <= maxLength)
                kur.Add(item.Value);
        }

        if (kur.Count == 0)
            throw new ArgumentException();

        return kur;
    }

    public IEnumerable<Person> GetWithNameSize(int length)
    {
        List<Person> kur = new List<Person>();

        foreach (var item in company)
        {
            if (item.Value.Name.Length == length)
                kur.Add(item.Value);
        }

        if(kur.Count == 0)
            throw new ArgumentException();

        return kur;
    }

    public IEnumerable<Person> PeopleByInsertOrder()
    {
        return company.Select(x => x.Value);
    }
}