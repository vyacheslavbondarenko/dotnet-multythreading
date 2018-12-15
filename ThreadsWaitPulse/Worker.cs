﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadsWaitPulse
{
    public class Worker
    {
        private int actionId = 1;
        private Data data = null;
        public Worker(Data _data, int _actionId) {
            data = _data;
            actionId = _actionId;
        }
        public void DoWork() {
            for (int i = 0; i < 5; i++)
            {
                /*//Console.WriteLine(i);
                Monitor.Enter(data);
                //Этого ли экземпляра очередь вызывать метод из объекта data?
                if (data.GetState() == actionId)
                {
                    //Если да, то какой именно метод вызывать?
                    if (actionId == 1)
                    {
                        data.Tik();
                    }
                    else {
                        data.Tac();
                    }
                }
                Monitor.Exit(data);*/

                //Monitor.Enter(data);
                //Этого ли экземпляра очередь вызывать метод из объекта data?
                lock (data)
                {
                    while (data.GetState() != actionId)
                    {
                        Monitor.Wait(data);
                    }
                    //Если да, то какой именно метод вызывать?
                    if (actionId == 1)
                    {
                        data.Tik();
                    }
                    else
                    {
                        data.Tac();
                    }
                    Monitor.Pulse(data);
                }
                //Monitor.Exit(data);
            }
        }
    }
}
