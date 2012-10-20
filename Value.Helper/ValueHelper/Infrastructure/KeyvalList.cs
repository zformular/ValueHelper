/*
  >>>------ Copyright (c) 2012 zformular ----> 
 |                                            |
 |            Author: zformular               |
 |        E-mail: zformular@163.com           |
 |             Date: 10.9.2012                |
 |                                            |
 ╰==========================================╯
 
*/

using System;
using System.Collections.Generic;
using System.Collections;

namespace ValueHelper.Infrastructure
{
    public class KeyvalList<TKey, TValue> : IEnumerable
    {
        private List<Keyval<TKey, TValue>> keyvalList;

        public KeyvalList()
        {
            keyvalList = new List<Keyval<TKey, TValue>>();
        }

        public Keyval<TKey, TValue> this[Int32 index]
        {
            get { return keyvalList[index]; }
        }

        public TValue this[String key]
        {
            get
            {
                for (int i = 0; i < keyvalList.Count; i++)
                {
                    if (key == keyvalList[i].Key.ToString())
                        return keyvalList[i].Value;
                }
                throw new InvalidOperationException("不存在键 " + key);
            }
        }

        public void Add(Keyval<TKey, TValue> keyval)
        {
            this.keyvalList.Add(keyval);
        }

        public void AddRange(IEnumerable<Keyval<TKey, TValue>> keyvals)
        {
            this.keyvalList.AddRange(keyvals);
        }

        public void Remove(Keyval<TKey, TValue> keyval)
        {
            this.keyvalList.Remove(keyval);
        }

        public void RemoveAt(Int32 index)
        {
            this.keyvalList.RemoveAt(index);
        }

        public void Clear()
        {
            this.keyvalList.Clear();
        }

        public String ToString(Char innerConnector, Char outerConnector)
        {
            String result = String.Empty;
            Int32 count = keyvalList.Count;
            for (int index = 0; index < count - 1; index++)
            {
                result += (keyvalList[index].Key + innerConnector.ToString() + keyvalList[index].Value + outerConnector.ToString());
            }
            if (keyvalList.Count > 0)
                result += (keyvalList[count - 1].Key + innerConnector.ToString() + keyvalList[count - 1].Value);
            return result;
        }

        public override string ToString()
        {
            return ToString(',', ';');
        }

        #region IEnumerable 成员

        public IEnumerator GetEnumerator()
        {
            return keyvalList.GetEnumerator();
        }

        #endregion
    }

    public class Keyval<TKey, TValue>
    {
        public TKey Key { get; set; }

        public TValue Value { get; set; }
    }
}
