#include"List&Stack.cpp"
#include <io.h>
#include <cstdlib>
#include <fcntl.h>
#include <windows.h>
#include<string>
#include<sstream>
#define GREEN    "\033[1;32m" 
#define RESET   "\033[0m"
#define MAGENTA    "\033[1;35m"
#define YELLOW  "\033[1;33m"


using namespace std;

class Card {
private:
	char suit;
	char rank;
	bool visible;

	// Print the suit symbol based on the suit character
	void getSuitSymbol(HANDLE hConsole) const {
		_setmode(_fileno(stdout), _O_U16TEXT);  // Set the output mode to Unicode to support suit symbols

		switch (suit) {
		case 'H':
			SetConsoleTextAttribute(hConsole, 4);
			wcout << L"♥";
			SetConsoleTextAttribute(hConsole, 7);
			break;
		case 'D':
			SetConsoleTextAttribute(hConsole, 4);
			wcout << L"♦";
			SetConsoleTextAttribute(hConsole, 7);
			break;
		case 'C':
			wcout << L"♣";
			break;
		case 'S':
			wcout << L"♠";
			break;
		default:
			wcout << L'?';
			break;
		}

		_setmode(_fileno(stdout), _O_TEXT);  // Reset to regular text mode
	}






public:

	Card(char s = '?', char r = '?', bool v = false) : suit(s), rank(r), visible(v) {}

	void displayCard() const {
		// Get the console handle
		HANDLE hConsole = GetStdHandle(STD_OUTPUT_HANDLE);

		if (visible) {
			if (rank == '1') {
				cout << 10;
			}
			else
				cout << rank;
			getSuitSymbol(hConsole);

		}
		else {
			cout << "[??]";
		}
	}

	void setVisible(bool v) {
		visible = v;
	}

	bool isVisible() const {
		return visible;
	}
	void Display() {

		displayCard();
	}

	char getColor() {

		switch (suit) {
		case 'H': return 'r'; // Hearts
		case 'D': return 'r'; // Diamonds
		case 'C': return 'b'; // Clubs
		case 'S': return 'b'; // Spades
		}

	}
	int getRank() {
		switch (rank) {
		case 'A': return 1;
		case '2': return 2;
		case '3': return 3;
		case '4': return 4;
		case '5': return 5;
		case '6': return 6;
		case '7': return 7;
		case '8': return 8;
		case '9': return 9;
		case '1': return 10; // 1 for 10
		case 'J': return 11; // Jack
		case 'Q': return 12; // Queen
		case 'K': return 13; // King

		}
	}


};

Card** getDeck() {

	Card** Deck = new Card * [52];
	int d_index = 0;
	char suits[] = { 'H', 'D', 'S', 'C' };

	for (int i = 0; i < 4; i++) {

		for (int j = 1; j <= 13; j++) {

			char rank;
			switch (j) {
			case 1: rank = 'A'; break;
			case 11: rank = 'J'; break;
			case 12: rank = 'Q'; break;
			case 13: rank = 'K'; break;
			case 10: rank = '1'; break;
			default: rank = j + '0'; break;
			}
			Deck[d_index] = new Card(suits[i], rank, false);
			d_index++;
		}
	}

	return Deck;
}
void shuffleDeck(Card** deck) {

	int size = 52;
	srand(static_cast<unsigned int>(time(0)));

	for (int i = size - 1; i > 0; i--) {
		int j = rand() % (i + 1);
		Card* temp = deck[i];
		deck[i] = deck[j];
		deck[j] = temp;
	}


}

void DeallocateDeck() {

}


template<typename T>
class Game {
private:
	Card** Deck;
	List<T>* Column;
	Stack<T>* Foundation;
	Stack<T> Stock;
	Stack<T> Waste;
public:
	Game() {
		Column = NULL;
		Foundation = NULL;
	}
	void SetupGame() {



		Column = new List<T>[7];
		Foundation = new Stack<T>[4];

		Deck = getDeck();
		shuffleDeck(Deck);
		int d_index = 0;

		for (int i = 0; i < 7; i++) {
			for (int j = 0; j < i + 1; j++) {
				Column[i].InsertAtTail(*Deck[d_index++]);
			}
			Column[i].End().current->prev->data.setVisible(true);

		}
		while (d_index < 52) {

			Stock.Push(*Deck[d_index++]);
		}


	}
	void DisplayGame() {
		int numColumns = 7, numFoundations = 4;

		cout << GREEN << "=========================================================================================================" << RESET << endl;
		cout << YELLOW << "Stock\t\t| Waste\t\t\t| Foundation 1\t| Foundation 2\t| Foundation 3\t| Foundation 4\t|" << RESET << endl;
		cout << GREEN << "=========================================================================================================" << RESET << endl;


		if (!Stock.isEmpty()) {
			cout << "[??]";
		}
		else {
			cout << "[  ]";
		}
		cout << "\t\t" << MAGENTA << "| " << RESET;


		if (!Waste.isEmpty()) {
			Card topWasteCard;
			Waste.Pop(topWasteCard);
			topWasteCard.Display();
			Waste.Push(topWasteCard);
		}
		else {
			cout << "[  ]";
		}
		cout << "\t\t\t" << MAGENTA << "| " << RESET;


		for (int i = 0; i < numFoundations; i++) {
			if (!Foundation[i].isEmpty()) {
				Card topFoundationCard;
				Foundation[i].Pop(topFoundationCard);
				topFoundationCard.Display();
				Foundation[i].Push(topFoundationCard);
			}
			else {
				cout << "[  ]";
			}
			cout << "\t\t" << MAGENTA << "| " << RESET;
		}
		cout << "\n";


		cout << "(" << Stock.getSize() << " cards)" << "\t" << MAGENTA << "| " << RESET;
		cout << "(" << Waste.getSize() << " cards)" << "\t\t" << MAGENTA << "| " << RESET;
		for (int i = 0; i < numFoundations; i++) {
			cout << "(" << Foundation[i].getSize() << " cards)";
			cout << "\t" << MAGENTA << "| " << RESET;
		}
		cout << "\n\n";


		int maxCards = 0;
		List<Card>::Iterator it;
		for (int i = 0; i < numColumns; i++) {
			int count = 0;
			for (it = Column[i].Begin(); it != Column[i].End(); it++) {
				count++;
			}
			if (count > maxCards) {
				maxCards = count;
			}
		}


		cout << GREEN << "=================================================================================================================" << RESET << endl;
		cout << YELLOW << "Column 1\t| Column 2\t| Column 3\t| Column 4\t| Column 5\t| Column 6\t| Column 7\t|" << RESET << endl;
		cout << GREEN << "-----------------------------------------------------------------------------------------------------------------" << RESET << endl;

		for (int row = 1; row <= maxCards; row++) {
			for (int i = 0; i < numColumns; i++) {
				if (Column[i].getSize() >= row) {
					it = Column[i].Begin();
					for (int j = 1; j < row; j++) it++;
					it.current->data.Display();
					cout << "\t\t";
				}
				else {
					cout << "\t\t";
				}
				cout << MAGENTA << "| " << RESET;
			}
			cout << endl;
		}
		cout << GREEN << "=================================================================================================================" << RESET << endl;
		cout << YELLOW << "*                 Valid Commands:                  *" << endl;
		cout << "*                                                  *" << endl;
		cout << "*   s            -> Draw from stockpile.           *" << endl;
		cout << "*   m src dest n -> Move n cards from src to dest. *" << endl;
		cout << "*   z            -> Undo last move.                *" << endl;
		cout << "*   e            -> Exit Game.                     *" << endl;
		cout << "*                                                  *" << RESET << endl;


	}

	void DeallocateDeck() {

		for (int i = 0; i < 52; i++) {
			delete Deck[i];
		}
		Deck = NULL;
	}

	friend class Command;
};

class Command {

private:

	string operation;
	string source;
	string destination;
	int num;
	Stack<string> UndoStack;

public:
	Command(string op = "\0", string s = "\0", string d = "\0", int n = 0) {
		operation = op;
		source = s;
		destination = d;
		num = n;

	}

	void TakeCommand() {
		string input;
		cout << "Enter command: ";
		getline(cin, input);


		stringstream ss(input);
		ss >> operation;


		if (operation == "m") {

			string src, dest;
			ss >> src >> dest >> num;
			source = src;
			destination = dest;
		}

	}
	void Draw(Game<Card>& G) {

		if (G.Stock.isEmpty()) {

			while (!G.Waste.isEmpty()) {
				Card TopWasteCard;
				G.Waste.Pop(TopWasteCard);
				TopWasteCard.setVisible(false);
				G.Stock.Push(TopWasteCard);
			}

		}
		if (G.Stock.isEmpty()) {
			cout << "Stock and Waste are empty ." << endl;
			return;
		}
		Card TopStockCard;
		G.Stock.Pop(TopStockCard);
		TopStockCard.setVisible(true);
		G.Waste.Push(TopStockCard);

		UndoStack.Push(operation);

	}

	void Undo(Game<Card>& G) {

		if (!UndoStack.isEmpty()) {

			UndoStack.Pop(operation);

			if (operation == "s") {

				Card TopWasteCard;
				G.Waste.Pop(TopWasteCard);
				TopWasteCard.setVisible(false);
				G.Stock.Push(TopWasteCard);

			}
			else {

				string n;
				UndoStack.Pop(n);
				num = stoi(n);
				UndoStack.Pop(source);
				UndoStack.Pop(destination);



				Card SourceCard;
				Card DestinationCard;
				int src_no = -1, dest_no = -1;


				// Array of valid source/destination names
				string moves[12] = { "c1","c2","c3","c4","c5","c6","c7","f1","f2","f3","f4","w" };
				int indices[12] = { 0,1,2,3,4,5,6,0,1,2,3,11 };

				// Validate the source
				for (int i = 0; i < 12; i++) {
					if (source == moves[i]) {
						src_no = i;
						// Source is waste
						if (src_no == 11) {
							src_no = i;

						}
						// Source is column
						else if (src_no < 7) {
							src_no = i;

						}
						// Source is foundation
						else if ((src_no >= 7 && src_no != 11)) {
							src_no = i;

						}
						break;
					}
				}

				// Validate the destination (columns and foundations)
				for (int i = 0; i < 12; i++) {
					if (destination == moves[i]) {
						dest_no = i;

						break;
					}
				}



				// Get the source card
				List<Card>::Iterator src_it, dest_it;
				if (src_no == 11) {
					// Source is waste
					G.Waste.Pop(SourceCard);
				}
				else if (src_no >= 7) {
					// Source is foundation
					G.Foundation[src_no - 7].Pop(SourceCard);
				}
				else {
					// Source is a column
					src_it = G.Column[src_no].End();
					for (int i = num; i > 0; i--) src_it--;
					SourceCard = src_it.current->data;
				}


				// Get the destination card 
				if (dest_no < 7) { // Destination is a column
					dest_it = G.Column[dest_no].End();
					dest_it--;
					DestinationCard = dest_it.current->data;

				}

				MoveCards(G, src_it, SourceCard, src_no, dest_no, num);
			}

		}
		else
		{
			cout << YELLOW << "No Previous Move Availabe" << RESET << endl;
			cin.ignore();
		}
	}
	bool IsDestinationValid(Game<Card>& G, Card SourceCard, Card DestinationCard, int dest_no) {

		// Check if the destination is a column
		if (dest_no < 7) {
			if (!G.Column[dest_no].isEmpty()) {
				// If column is not empty, check that the suits are different and rank is one higher
				if ((DestinationCard.getColor() == SourceCard.getColor()) ||
					(DestinationCard.getRank() != SourceCard.getRank() + 1)) {
					return false;
				}
			}
			else {
				// If column is empty, only allow a King (rank 13) to be placed
				if (SourceCard.getRank() != 13) {
					return false;
				}
			}
		}
		// Check if the destination is a foundation
		else {
			bool check = false;  // To track if we popped from the foundation


			if (!G.Foundation[dest_no - 7].isEmpty()) {

				// If foundation is not empty, check that the suits are the same and rank is one lower
				G.Foundation[dest_no - 7].Pop(DestinationCard);
				check = true;
				if ((DestinationCard.getColor() != SourceCard.getColor()) ||
					(DestinationCard.getRank() + 1 != SourceCard.getRank())) {
					if (check) {
						G.Foundation[dest_no - 7].Push(DestinationCard);
					}
					return false;
				}
			}
			else {
				// If foundation is empty, only allow an Ace (rank 1) to be placed
				if (SourceCard.getRank() != 1) {
					return false;
				}
			}
			if (check) {
				G.Foundation[dest_no - 7].Push(DestinationCard);
			}
		}

		// If all checks pass, the move is valid
		return true;
	}

	bool IsSourceValid(List<Card>::Iterator src_it, Game<Card>& G, int src_no) {

		List<Card>::Iterator it = src_it, it1 = src_it;

		for (; it1 != G.Column[src_no].End(); ++it1) {

			Card c_Card = it1.current->data;
			if (!c_Card.isVisible()) {

				return false;

			}

		}

		for (; it != G.Column[src_no].End(); ++it) {
			List<Card>::Iterator next_it = it;
			++next_it;

			if (next_it == G.Column[src_no].End()) break;

			Card currentCard = it.current->data;
			Card nextCard = next_it.current->data;


			if (currentCard.getColor() == nextCard.getColor()) {
				return false;
			}


			if (currentCard.getRank() <= nextCard.getRank()) {
				return false;
			}
		}


		return true;
	}

	void MoveCards(Game<Card>& G, List<Card>::Iterator src_it, Card SourceCard, int src_no, int dest_no, int num) {



		//Source is Waste
		if (src_no == 11) {

			G.Column[dest_no].InsertAtTail(SourceCard);

		}
		else if (dest_no == 11) {
			G.Waste.Push(SourceCard);
			G.Column[src_no].RemoveAtTail();
		}
		//Source is column and destination is foundation
		else if (dest_no >= 7 && src_no < 7) {

			if (src_it.current->prev != NULL) {
				src_it.current->prev->data.setVisible(true);
			}
			Card movingCard = src_it.current->data;
			G.Foundation[dest_no - 7].Push(movingCard);
			G.Column[src_no].RemoveAtTail();

		}
		//Destination is column and source is foundation
		else if (dest_no < 7 && src_no >= 7) {

			G.Column[dest_no].InsertAtTail(SourceCard);
		}
		//Both are column
		else {

			if (src_it.current->prev != NULL) {
				src_it.current->prev->data.setVisible(true);
			}

			G.Column[src_no].MoveSubList(G.Column[dest_no], num);

		
		}


	}


	bool Move(Game<Card>& G) {

		Card SourceCard;
		Card DestinationCard;
		int src_no = -1, dest_no = -1;
		bool s_valid = false, d_valid = false;

		// Array of valid source/destination names
		string moves[12] = { "c1","c2","c3","c4","c5","c6","c7","f1","f2","f3","f4","w" };
		int indices[12] = { 0,1,2,3,4,5,6,0,1,2,3,11 };

		// Validate the source
		for (int i = 0; i < 12; i++) {
			if (source == moves[i]) {
				src_no = i;
				// Source is waste
				if (src_no == 11 && num == 1 && !G.Waste.isEmpty()) {
					src_no = i;
					s_valid = true;
				}
				// Source is column
				else if (src_no < 7 && G.Column[src_no].getSize() >= num) {
					src_no = i;
					s_valid = true;
				}
				// Source is foundation
				else if ((src_no >= 7 && src_no != 11) && num == 1 && !G.Foundation[src_no - 7].isEmpty()) {
					src_no = i;
					s_valid = true;
				}
				break;
			}
		}

		// Validate the destination (columns and foundations)
		for (int i = 0; i < 11; i++) {
			if (destination == moves[i]) {
				dest_no = i;
				d_valid = true;
				break;
			}
		}

		if (src_no == 11 && dest_no >= 7) {
			d_valid = false;
		}

		// If either source or destination is invalid, return false
		if (!(s_valid && d_valid)) {
			return false;
		}

		// Get the source card
		List<Card>::Iterator src_it, dest_it;
		if (src_no == 11) {
			// Source is waste
			G.Waste.Pop(SourceCard);
		}
		else if (src_no >= 7) {
			// Source is foundation
			G.Foundation[src_no - 7].Pop(SourceCard);
		}
		else {
			// Source is a column
			src_it = G.Column[src_no].End();
			for (int i = num; i > 0; i--) src_it--;
			SourceCard = src_it.current->data;
		}


		// Get the destination card 
		if (dest_no < 7) { // Destination is a column
			if (!G.Column[dest_no].isEmpty()) {
				dest_it = G.Column[dest_no].End();
				dest_it--;
				DestinationCard = dest_it.current->data;
			}
		}



		// Check if the move is legal based on card values and colors
		if (!IsDestinationValid(G, SourceCard, DestinationCard, dest_no)) {
			// If move is invalid, push the source and destination card back to the original location
			if (src_no == 11) {
				G.Waste.Push(SourceCard);
			}

			else if (src_no >= 7) {
				G.Foundation[src_no - 7].Push(SourceCard);
			}
			return false;
		}



		// If source is column, perform further checks for validity
		if (src_no < 7) {
			if (!IsSourceValid(src_it, G, src_no)) {

				return false;
			}
		}



		UndoStack.Push(source);
		UndoStack.Push(destination);
		UndoStack.Push(to_string(num));
		UndoStack.Push(operation);

		MoveCards(G, src_it, SourceCard, src_no, dest_no, num);


		return true;
	}

	void ProcessCommand(Game<Card>& G) {
		if (operation == "s") {

			Draw(G);
			num = 0;
			operation = "\0";
		}
		else if (operation == "m") {

			bool valid = Move(G);
			num = 0;
			operation = "\0";
			if (!valid) {
				cout << "Invalid Move" << endl;
				cin.ignore();

				return;
			}

		}
		else if (operation == "z") {
			Undo(G);
		}
		else if (operation == "e") {
			cout << YELLOW << "Game Exited" << RESET << endl;
			DeallocateDeck();
			exit(0);
		}
		else {

			cout << "Invalid Move" << endl;
			cin.ignore();
		}
	}
	bool GameDecision(Game<Card> G) {
		// Check for winning condition



		bool win = true;
		for (int i = 0; i < 4; i++) {
			if (G.Foundation[i].getSize() != 13) {
				win = false;
				break;
			}
		}

		if (win) {
			system("cls");
			cout << YELLOW << "\n*******************************************" << endl;
			cout << "*           CONGRATULATIONS!                *" << endl;
			cout << "*                                           *" << endl;
			cout << "*       You have won the game!              *" << endl;
			cout << "*                                           *" << endl;
			cout << "*       Thank you for playing!              *" << endl;
			cout << "*******************************************" << RESET << endl;

			return false;
		}
		else return true;

	}

};

void StartupScreen() {

	cout << GREEN << "*****************************************************************************" << endl;
	cout << "*                                                                           *" << endl;
	cout << "*                         Welcome to Console Solitaire!                     *" << endl;
	cout << "*                                                                           *" << endl;
	cout << MAGENTA << "*                   Objective: Move all cards to Foundation.                *" << RESET << endl;
	cout << "*                                                                           *" << endl;
	cout << YELLOW << "*                                Game Rules:                                *" << endl;
	cout << "*                   1. Move cards to columns in descending,                 *" << endl;
	cout << "*                            alternating color order.                       *" << endl;
	cout << "*                 2. Only Kings can be placed in empty cols.                *" << endl;
	cout << "*                 3. Foundation piles must be filled from                   *" << endl;
	cout << "*                        Ace to King in matching suits.                     *" << endl;
	cout << "*                4. Draw one card at a time from stockpile.                 *" << endl;
	cout << "*                                                                           *" << endl;
	cout << "*****************************************************************************" << RESET << endl;

}

int main() {


	Game<Card> G;
	Command cmd;

	G.SetupGame();
	StartupScreen();
	cin.ignore();
	system("cls");


	while (cmd.GameDecision(G)) {

		G.DisplayGame();
		cmd.TakeCommand();
		cmd.ProcessCommand(G);
		system("cls");
	}

	G.DeallocateDeck();
}
