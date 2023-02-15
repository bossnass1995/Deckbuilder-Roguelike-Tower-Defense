using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

  List<Card> deck;


  [SerializeField] Overlay overlay;
  [SerializeField] Hand hand;
  [SerializeField] DiscardZone discard;
  [SerializeField] Grid grid;

  // Hardcoded card prefabs
  [SerializeField] Card BALLISTA;
  [SerializeField] Card KRAKAPULT;

  /**
  Deck rules:
    - Decks will be composed of 5 cards (subject to change of course)
    - Each card is assigned a "bottom"
      We still haven't decided if all cards come with the "bottom" already assigned or if they will be randomly assigned
      once the game starts. I'd personally lean toward having the bottom already assigned, or at least part of the deck
      building process
    - Each card is duplicated (2 copies in each deck) with one copy having the "draw 2" effect
    - Cards are shuffled
    - The player draws 3 cards to start the game
  */

  public void Awake() {
    // For now we are going to hardcode one deck. In the future, the deck will be injected based on a deck builder
    Debug.Log("Starting game and filling deck");
    deck = new List<Card>();
    deck.Add(BALLISTA);
    deck.Add(KRAKAPULT);
    Shuffle();
    var card1 = Draw();
    var card2 = Draw();
    var card3 = Draw();
  }

  public void Shuffle() {
    for (int i = 0; i < deck.Count; i++) {
      Card tmp = deck[i];
      int r = Random.Range(i, deck.Count);
      deck[i] = deck[r];
      deck[r] = tmp;
    }
  }

  public Card Draw() {
    if (deck.Count > 0) {
      var prefab = deck[0];
      deck.Remove(prefab);
      var card = Instantiate(prefab, Vector3.one, Quaternion.identity);
      card.gameObject.transform.SetParent(hand.transform);
      card.overlay = overlay;
      card.hand = hand;
      card.discard = discard;
      card.grid = grid;
      return card;
    }
    return null;
  }
}