﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using OpinionSharing.Subject;
using MyRandom;

namespace OpinionSharing.Agt.Algorithm
{
    public class PartialLimitedBelief : AAT //NoMoreの上位互換。h_trg=1のとき、チート。
    {


        public override void Initialize()
        {
            base.Initialize();
        }


        public override void ProcessMessage(BWMessage message)
        {
            //センサーからのメッセージならば、センサーの精度分信じる
            if (message.From != null && message.From.Accuracy != null)
            {
                UpdateOpinion(message.Subject, new BeliefUpdater( message.From.Accuracy.Value));//BlackWhiteSubject,double
            }

            //それ以外は、自分のImportanceLevelを使う
            else
            {
                // 意見が白でbeliefが0.8以上とかだったら、更新しない。
                if (message.Subject == BlackWhiteSubject.White && Belief >= Sigma ||
                    message.Subject == BlackWhiteSubject.Black && Belief <= 1 - Sigma)
                {
                    UpdateOpinion(message.Subject, new BeliefUpdater( 0.5 )); //重要視度0で更新する。
                }
                else
                {
                    UpdateOpinion(message.Subject, BeliefUpdater);
                }
            }
        }
    }
}
