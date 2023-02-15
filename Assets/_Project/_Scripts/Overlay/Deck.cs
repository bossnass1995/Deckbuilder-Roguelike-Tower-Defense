using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

  private List<Card> deck;

  [SerializeField] Hand hand;

  [SerializeField] private Card BALLISTA;
  [SerializeField] private Card KRAKAPULT;

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
    deck = new List<Card>();
    deck.Add(BALLISTA);
    deck.Add(KRAKAPULT);
    Shuffle();
    var card1 = Draw();
    var card2 = Draw();
    var card3 = Draw();
    card1.gameObject.transform.SetParent(hand.transform);
    card2.gameObject.transform.SetParent(hand.transform);
    card3.gameObject.transform.SetParent(hand.transform);
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
      var card = deck[0];
      deck.Remove(card);
      return card;
    }
    return null;
  }
}