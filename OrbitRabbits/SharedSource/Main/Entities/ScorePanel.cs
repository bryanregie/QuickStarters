﻿#region File Description
//-----------------------------------------------------------------------------
// OrbitRabbits
//
// Quickstarter for Wave University Tour 2014.
// Author: Wave Engine Team
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using OrbitRabbits.Commons;
using OrbitRabbits.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveEngine.Common.Math;
using WaveEngine.Components;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Services;
using WaveEngine.Framework.UI;
#endregion

namespace OrbitRabbits.Entities
{
    public class ScorePanel : BaseDecorator
    {
        private int scores;        
        private TextBlock scoreText, bestText;
        private GameStorage gameStorage;

        #region Properties
        /// <summary>
        /// Gets or sets the horizontal alignment.
        /// </summary>
        public HorizontalAlignment HorizontalAlignment
        {
            get
            {
                return this.entity.FindComponent<PanelControl>().HorizontalAlignment;
            }

            set
            {
                this.entity.FindComponent<PanelControl>().HorizontalAlignment = value;
            }
        }

        /// <summary>
        /// Gets or sets the vertical alignment.
        /// </summary>
        public VerticalAlignment VerticalAlignment
        {
            get
            {
                return this.entity.FindComponent<PanelControl>().VerticalAlignment;
            }

            set
            {
                this.entity.FindComponent<PanelControl>().VerticalAlignment = value;
            }
        }

        /// <summary>
        /// Gets or sets the Margin.
        /// </summary>
        public Thickness Margin
        {
            get
            {
                return this.entity.FindComponent<PanelControl>().Margin;
            }

            set
            {
                this.entity.FindComponent<PanelControl>().Margin = value;
            }
        }

        /// <summary>
        /// Gets or sets the scores.
        /// </summary>
        public int Scores
        {
            get { return this.scores; }
            set
            {
                this.scores = value;
                this.scoreText.Text = this.scores.ToString();
                if (this.gameStorage.BestScore < this.scores)
                {
                    this.gameStorage.BestScore = this.scores;
                    this.bestText.Text = this.gameStorage.BestScore.ToString();
                }
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ScorePanel" /> class.
        /// </summary>
        /// <param name="name">Entity name</param>
        public ScorePanel(string name)
        {
            this.scores = 1;

            this.gameStorage = Catalog.GetItem<GameStorage>();

            this.entity = new Entity(name)
                          .AddComponent(new Transform2D())
                          .AddComponent(new PanelControl(241, 104))
                          .AddComponent(new PanelControlRenderer())
                           .AddChild(new Image(WaveContent.Assets.Textures.scorePanel_png)
                           {
                               DrawOrder = 0.4f,
                           }.Entity);
            this.scoreText = new TextBlock("scoresText")
             {
                 Width = 40,
                 Text = this.Scores.ToString(),
                 FontPath = WaveContent.Assets.Fonts.OCR_A_Extended_16_TTF,
                 HorizontalAlignment = HorizontalAlignment.Right,
                 VerticalAlignment = VerticalAlignment.Top,
                 Margin = new Thickness(0, 15, 50, 0),
                 DrawOrder = 0.2f,
             };
            this.entity.AddChild(this.scoreText.Entity);
            this.bestText = new TextBlock("bestText")
             {
                 Width = 40,
                 Text = this.gameStorage.BestScore.ToString(),
                 FontPath = WaveContent.Assets.Fonts.OCR_A_Extended_16_TTF,
                 HorizontalAlignment = HorizontalAlignment.Right,
                 VerticalAlignment = VerticalAlignment.Bottom,
                 Margin = new Thickness(0, 0, 50, 5),
                 DrawOrder = 0.2f,
             };
            this.entity.AddChild(this.bestText.Entity);
        }
    }
}
