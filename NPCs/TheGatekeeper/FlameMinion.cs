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
    public class FlameMinion : ModNPC
    {
        public override void SetDefaults()
        {

            npc.damage = 90;
            npc.npcSlots = 1f;
            npc.width = 38; //324
            npc.height = 36; //216
            npc.defense = 0;
            npc.lifeMax = 1000;
            npc.aiStyle = 14; //new
            aiType = -1; //new
            npc.knockBackResist = 0f;
            npc.value = Item.buyPrice(0, 10, 0, 0);
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath6;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            var effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame, drawColor, npc.rotation, npc.frame.Size() / 2, npc.scale, effects, 0);
            Texture2D glowmask = mod.GetTexture("NPCs/TheGatekeeper/FlameMinion_Glowmask");
            spriteBatch.Draw(glowmask, npc.Center - Main.screenPosition, npc.frame, npc.GetAlpha(Color.White), npc.rotation, npc.frame.Size() / 2, npc.scale, effects, 0);
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flame Apparation");
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 14);
                int num3;
                int num4;
                for (int num624 = 0; num624 < 50; num624 = num3 + 1)
                {
                    int num625 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 127, 0f, 0f, 0, default(Color), 2f);
                    Dust dust = Main.dust[num625];
                    dust.velocity *= 2.5f;
                    dust = Main.dust[num625];
                    dust.scale *= 0.9f;
                    Main.dust[num625].noGravity = true;
                    num3 = num624;
                }
                int num11 = Main.rand.Next(2, 3);
                for (int j = 0; j < num11; j++)
                {
                    Vector2 vector5 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                    vector5 += npc.velocity * 3f;
                    vector5.Normalize();
                    vector5 *= (float)Main.rand.Next(35, 39) * 0.1f;

                    int dab = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, vector5.X, vector5.Y, 15, 40, 0f, 0);
                    Main.projectile[dab].hostile = true;
                    Main.projectile[dab].friendly = false;
                }

                int num12 = Main.rand.Next(2, 3);
                for (int g = 0; g < num11; g++)
                {
                    Vector2 vector5 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                    vector5 += npc.velocity * 3f;
                    vector5.Normalize();
                    vector5 *= (float)Main.rand.Next(35, 39) * 0.1f;

                    int dab = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, vector5.X, vector5.Y, 328, 40, 0f, 0);
                    Main.projectile[dab].hostile = true;
                    Main.projectile[dab].friendly = false;
                }
            }
        }
        public override void AI()
        {
            Player player = Main.player[npc.target];
            npc.noGravity = true;
            npc.TargetClosest(true);
            npc.ai[0] += 1;
            if (npc.ai[0] >= 150 && Main.netMode != 1) // small leaf
            {
                npc.ai[0] = 0;
                float Speed = 11f;  // projectile speed
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 4), npc.position.Y + (npc.height / 4));
                int damage = 24;  // projectile damage
                int type = mod.ProjectileType("FireBolt");  //put your projectile
                float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);
                npc.netUpdate = true;
            }
        }
    }
}
