using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            int nLines = 6;
            string[] input = {"Elizabeth - 1926 - Anglican F",
                           "Charles Elizabeth 1948 - Anglican M",
                           "William Charles 1982 - Anglican M",
                           "George William 2013 - Anglican M",
                           "Charlotte William 2015 - Anglican F",
                           "Henry Charles 1984 - Anglican M"};
            var family = Family.Parse(nLines, input);
            string expected = $"Elizabeth\r\nCharles\r\nWilliam\r\nGeorge\r\nCharlotte\r\nHenry";
            Assert.AreEqual(expected, family.ToString());
        }

        [Test]
        public void Test2()
        {
            int nLines = 6;
            string[] input = {"Elizabeth - 1926 - Anglican F",
                              "Charles Elizabeth 1948 - Anglican M",
                              "William Charles 1982 - Anglican M",
                              "George William 2013 - Anglican M",
                              "Charlotte William 2015 - Anglican F",
                              "Henry Charles 1984 - Anglican M",
                              "Andrew Elizabeth 1960 - Anglican M",
                              "Beatrice Andrew 1988 - Anglican F",
                              "Eugenie Andrew 1990 - Anglican F",
                              "Edward Elizabeth 1964 - Anglican M",
                              "James Edward 2007 - Anglican M",
                              "Louise Edward 2003 - Anglican F",
                              "Anne Elizabeth 1950 - Anglican F",
                              "Peter Anne 1977 - Anglican M",
                              "Savannah Peter 2010 - Anglican F",
                              "Isla Peter 2012 - Anglican F",
                              "Zara Anne 1981 - Anglican F",
                              "Mia Zara 2014 - Anglican F"};
            var family = Family.Parse(nLines, input);
            string expected = $"Elizabeth\r\nCharles\r\nWilliam\r\nGeorge\r\nCharlotte\r\nHenry\r\nAndrew\r\nEdward\r\n";
            Assert.AreEqual(expected, family.ToString());
        }
    }

    public class Family
    {
        private Person _root;

        public static object Parse(int nLines, string[] input)
        {
            Family family = new Family();
            for (int i = 0; i < nLines; i++) {
                string[] inputs = input[i].Split(' ');
                string name = inputs[0];
                Person parent = family.FindPerson(inputs[1]);
                int birth = int.Parse(inputs[2]);
                string death = inputs[3];
                string religion = inputs[4];
                string gender = inputs[5];

                family.AddPerson(new Person(name, parent, birth, death, religion, gender));
            }
            return family;
        }

        public void AddPerson(Person person)
        {
            if (_root == null) {
                _root = person;
            } else {
                _root.AddPerson(person);
            }
        }

        public Person FindPerson(string name)
        {
            Person result = null;
            if (_root != null) {
                result = _root.FindPerson(name);
            }
            return result;
        }

        public override string ToString() {
            return string.Join(Environment.NewLine, _root.OrderOfSuccession());
        }
    }

    public class Person {
        public enum GenderEnum {
            M,
            F
        }

        private PersonComparer _comparer = new PersonComparer();
        public Person() {
            Children = new List<Person>();
        }
        public Person(string name, Person parent, int birth, string death, string religion, string gender): this() {
            Name=name;
            Parent = parent;
            BirthYear = birth;
            DeathYear = death;
            Religion = religion;
            Gender = (GenderEnum)Enum.Parse(typeof(GenderEnum), gender);
        }
        public string Name {get;set;}
        public Person Parent {get;set;}
        public int BirthYear {get;set;}
        public string DeathYear {get;set;}
        public string Religion {get;set;}
        public GenderEnum Gender {get;set;}
        public List<Person> Children {get;set;}

        public Person FindPerson(string name)
        {
            Person result = null;
            if (Name.Equals(name)) {
                result = this;
            } else {
                var child = Children.GetEnumerator();
                while (child.MoveNext() && result == null) {
                    result = child.Current.FindPerson(name);
                }
            }
            return result;
        }

        public bool AddPerson(Person person)
        {
            bool result = true;
            if (person.Parent == this) {
                AddChild(person);
            } else {
                result = false;
                var child = Children.GetEnumerator();
                while (child.MoveNext() && !result) {
                    result = child.Current.AddPerson(person);
                }
            }
            return result;
        }

        private void AddChild(Person person)
        {
            Children.Add(person);
            Children.Sort(_comparer);
        }

        public List<string> OrderOfSuccession() {
            List<string> result = new List<string>();
            if (DeathYear.Equals("-") && Religion.Equals("Anglican"))
                result.Add(Name);
            foreach (var child in Children) {
                result.AddRange(child.OrderOfSuccession());
            }
            return result;
        }

        public class PersonComparer : IComparer<Person>
        {
            public int Compare(Person x, Person y)
            {
                int result = x.Gender.CompareTo(y.Gender);
                if (result == 0) {
                    result = x.BirthYear.CompareTo(y.BirthYear);
                }
                return result;
            }
        }
    }
}