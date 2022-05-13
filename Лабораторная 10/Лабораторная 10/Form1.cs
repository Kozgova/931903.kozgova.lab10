using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Лабораторная_10
{
    public partial class Form1 : Form
    {

        Random rnd = new Random();
        const int TeamAmount = 6;
        string[] TeamName = new string[TeamAmount] { "Томь", "Спартак", "Динамо", "Челси", "ЦСКА", "Зенит" };
        
        const double Win = 2;
        const double Draw = 1;
        const double Loss = 0;

        List <Match> MatchesInTournir;
        Team[] TeamsInTournir;
         
        struct Team
        {
            public string name;
            public int game;
            public int goal;
            public int win;
            public int draw;
            public int loss;
            public double points;
        }

        struct Match
        {
            public Team team1, team2;
            public int goal1, goal2;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void btStart1_Click(object sender, EventArgs e)
        {
            
            TeamsInTournir = new Team[TeamAmount];
            MatchesInTournir = new List <Match> ();

            
            for (int i = 0; i < TeamAmount; i++)
            {
                TeamsInTournir[i] = new Team() { name = TeamName[i] };
            }
            
            // матч
            int goalPlayer1, goalPlayer2;
            for (int Player1 = 0; Player1 < TeamAmount; Player1++)
            {
                for (int Player2 = 0; Player2 < TeamAmount; Player2++)
                {
                    if (Player1 != Player2) 
                    {
                        goalPlayer1 = GoalsInMatch(rnd.NextDouble()); 
                        goalPlayer2 = GoalsInMatch(rnd.NextDouble());

                        Match newMatch = new Match() 
                        {
                            team1 = TeamsInTournir[Player1],
                            team2 = TeamsInTournir[Player2],
                            goal1 = goalPlayer1,
                            goal2 = goalPlayer2
                        };
                        
                        MatchesInTournir.Add(newMatch); 

                        //данные для таблицы
                        TeamsInTournir[Player1].goal += goalPlayer1;
                        TeamsInTournir[Player2].goal += goalPlayer2;

                        TeamsInTournir[Player1].game++;
                        TeamsInTournir[Player2].game++;

                        if (goalPlayer1 > goalPlayer2)
                        {
                            TeamsInTournir[Player1].win++;
                            TeamsInTournir[Player2].loss++;
                        }
                        else if (goalPlayer1 < goalPlayer2)
                        {
                            TeamsInTournir[Player1].loss++;
                            TeamsInTournir[Player2].win++;
                        }
                        else
                        {
                            TeamsInTournir[Player1].draw++;
                            TeamsInTournir[Player2].draw++;
                        }
                    }
                }
            }

            for (int i = 0; i < TeamAmount; i++)
            {
                TeamsInTournir[i].points = TeamsInTournir[i].win * Win + TeamsInTournir[i].draw * Draw + TeamsInTournir[i].loss * Loss;
            }

            //вывод результатов
            for (int i = 0; i < TeamAmount; i++)
            {
                View1.Rows.Add
                   (TeamsInTournir[i].name,
                    TeamsInTournir[i].game,
                    TeamsInTournir[i].win,
                    TeamsInTournir[i].draw,
                    TeamsInTournir[i].loss,
                    TeamsInTournir[i].goal,
                    TeamsInTournir[i].points);
            }

            //вывод результатов
            foreach (var match in MatchesInTournir)
            {
                View2.Rows.Add
                   (match.team1.name,
                    match.team2.name,
                    $"{ match.goal1 }:{ match.goal2 }");
            }
        }

        // забитые голы
        private int GoalsInMatch(double l)
        {
            int m = 0;
            double S = Math.Log(rnd.NextDouble() * 10);

            while (S > -l)
            {
                S += Math.Log(rnd.NextDouble());
                m++;
            }

            return m;
        }

    }
    
}
