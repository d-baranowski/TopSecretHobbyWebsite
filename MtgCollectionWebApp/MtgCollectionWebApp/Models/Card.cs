using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MtgCollectionWebApp.Models
{
    public class Card
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual int CardId { get; set; }
        public virtual string CardArtist { get; set; } //The artist of the card. Ex: "Alan Pollack"
        public virtual int CardCmc { get; set; } //Converted Mana Cost
        public virtual string CardColors { get; set; } //Array of card colors. Ex: [ "Blue", "Green" ]
        public virtual string CardFlavour { get; set; } //The flavor text of the card.
        public virtual string CardForeignNames { get; set; } //Foreign language names for the card. Ex: [ { language : "Italian", name : "Wurm Devastatore" } ]
        public virtual string  CardHand { get; set; } //Maximum hand size modifier. Only exists for Vanguard cards. Ex: -2
        public virtual string CardImageUrl { get; set; } 
        public virtual string CardLayout { get; set; } //Options: "normal", "split", "flip", "double-faced", "token", "plane", "scheme", "phenomenon", "leveler", "vanguard"
        public virtual string CardLegalities { get; set; } //Ex: { "Legacy" : "Banned", "Vintage" : "Restricted", "Commander" : "Special (Banned as Commander)" }
        public virtual string CardLife { get; set; } //Starting life total modifier. Only exists for Vanguard cards. Ex: -5
        public virtual string CardLoyalty { get; set; } //The loyalty of the card. This is only present for planeswalkers. Ex: 5
        public virtual string CardManaCost { get; set; } //The mana cost of this card. Consists of one or more mana symbols. Ex: "{X}{1}{B}"
        public virtual int CardMultiverseId { get; set; }
        public virtual string CardName { get; set; }
        public virtual string CardNames { get; set; } //Only used for split, flip and dual cards. Will contain all the names on this card, front or back. Ex: [ "Research", "Development" ]
        public virtual string CardPower { get; set; } //The power of the card. This is only present for creatures. This is a string, not an integer. Ex: "1+*"
        public virtual string CardPrintings { get; set; } //The sets that this card was printed in. Ex: ["Tempest"]
        public virtual string CardRarity { get; set; } //The rarity of the card. Options: "Common", "Uncommon", "Rare", "Mythic Rare", "Special"
        public virtual string  CardSet { get; set; } //3 character code of the cards set. Ex: "JOU"
        public virtual string CardSubTypes { get; set; } //The subtypes of the card. These appear to the right of the dash in a card type. Usually each word is it's own subtype. Options: "Trap", "Arcane", "Equipment", "Aura", "Human", "Rat", "Squirrel", etc.
        public virtual string CardSuperTypes { get; set; } //The supertypes of the card. These appear to the far left of the card type. Options: "Basic", "Legendary", "Snow", etc.
        public virtual string CardText { get; set; } //The text of the card. May contain mana symbols and other symbols. This is the Oracle Text
        public virtual string CardToughness { get; set; } //The toughness of the card. This is only present for creatures. This is a string, not an integer Ex: "1+*"
        public virtual string CardType { get; set; } //The card type. This is the type you would see on the card if printed today. Note: The dash is a UTF8 'long dash' as per the MTG rules
        public virtual string CardTypes { get; set; } //Array card types. These appear to the left of the dash in a card type. Options: "Instant", "Sorcery", "Artifact", "Creature", "Enchantment", "Land", "Planeswalker" Ex: [ "Creature" ]
        public virtual string CardVariations { get; set; } //If a card has alternate art (for example, 4 different Forests, or the 2 Brothers Yamazaki) then each other variation's multiverseid will be listed here, NOT including the current card's multiverseid. Ex: [ 1909, 1910 ]
        public virtual string CardWatermark { get; set; } //The watermark on the card. Note: Split cards don't currently have this field set, despite having a watermark on each side of the split card. Ex: "Selesnya"
        public virtual string CardUrl { get; set; } //Url to the card on mtgapi.com
    }
}