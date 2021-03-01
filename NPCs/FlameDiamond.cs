using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;

namespace EnduriumMod.NPCs.TheGatekeeper
{
    public class FlameDiamond : ModNPC
    {
        public override void SetDefaults()
        {

            npc.damage = 50;
            npc.npcSlots = 1f;
            npc.width = 54; //324
            npc.height = 98; //216
            npc.defense = 10;
            npc.lifeMax = 8000;
            npc.aiStyle = -1; //new
            aiType = -1; //new
            npc.knockBackResist = 0f;
            npc.value = Item.buyPrice(0, 20, 0, 0);
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.DeathSound = SoundID.NPCDeath6;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flame Diamond");
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            var effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame, drawColor, npc.rotation, npc.frame.Size() / 2, npc.scale, effects, 0);
            Texture2D glowmask = mod.GetTexture("NPCs/TheGatekeeper/FlameDiamond");
            spriteBatch.Draw(glowmask, npc.Center - Main.screenPosition, npc.frame, npc.GetAlpha(Color.White), npc.rotation, npc.frame.Size() / 2, npc.scale, effects, 0);
            return false;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 90;
            npc.defense = 10;
        }
        public override bool PreAI()
        {
            npc.dontTakeDamage = true;
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            NPC p = Main.npc[NPC.FindFirstNPC(mod.NPCType("TheFlameWrath"))];
            //Factors for calculations
            double deg = (double)npc.ai[1]; //The degrees, you can multiply npc.ai[1] to make it orbit faster, may be choppy depending on the value
            double rad = deg * (Math.PI / 180); //Convert degrees to radians
            double dist = 250; //Distance away from the player

            /*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the npc's width   /
            /and height divided by two so the center of the npc is at the right place.     */
            npc.position.X = p.Center.X - (int)(Math.Cos(rad) * dist) - npc.width / 2;
            npc.position.Y = p.Center.Y - (int)(Math.Sin(rad) * dist) - npc.height / 2;
            if (Main.player[npc.target].dead)
            {
                npc.velocity.Y -= 1f;
                npc.timeLeft -= 1;
            }
            else
            {
                npc.timeLeft = 50;
            }
            npc.ai[1] += 1f;
            npc.noGravity = true;
            npc.TargetClosest(true);
            npc.ai[0] += 1;
            if (npc.ai[0] >= 125 && Main.netMode != 1) // small leaf
            {
                npc.ai[0] = 0;
                float Speed = 11f;  // projectile speed
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 4), npc.position.Y + (npc.height / 4));
                int damage = 34;  // projectile damage
                int type = mod.ProjectileType("FireBolt");  //put your projectile
                float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);
                npc.netUpdate = true;
            }
            //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
            return false;
        }
    }
}