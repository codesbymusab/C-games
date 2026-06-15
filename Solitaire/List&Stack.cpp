#include <iostream>
using namespace std;

template <class T>
class List {
private:
	class Node {
	public:
		Node* next;
		Node* prev;
		T data;
		Node() {
			next = NULL;
			prev = NULL;
		}
		Node(T d, Node* n = NULL, Node* p = NULL) {
			data = d;
			next = n;
			prev = p;
		}
	};

	Node* head;
	Node* tail;
	int size;
public:
	List() {
		head = new Node;
		tail = new Node;
		head->next = tail;
		tail->prev = head;
		size = 0;
	}

	void InsertAtHead(T d) {
		Node* t = new Node(d, head->next, head);
		head->next->prev = t;
		head->next = t;
		size++;
	}

	void InsertAtTail(T d) {
		Node* t = new Node(d, tail, tail->prev);
		tail->prev->next = t;
		tail->prev = t;
		size++;
	}

	void RemoveAtHead() {
		if (head->next == tail) {
			cout << "List is empty!" << endl;
			return;
		}
		Node* t = head->next;
		head->next = t->next;
		head->next->prev = head;
		size--;
		delete t;
	}

	void RemoveAtTail() {
		if (tail->prev == head) {
			cout << "List is empty!" << endl;
			return;
		}
		Node* t = tail->prev;
		tail->prev = t->prev;
		t->prev->next = tail;
		size--;
		delete t;
	}
	T getHead() {

		if (head->next != tail) {
			return head->next->data;
		}

	}
	int getSize() {
		return size;
	}
	void viewList() {
		Node* start = head->next;
		cout << "List:" << endl;
		while (start != tail) {

			cout << start->data << " ";
			start = start->next;
		}
		cout << endl;
	}
	bool isEmpty() {
		if (size == 0) {
			return true;
		}
		else return false;
	}

	

		void MoveSubList(List<T>& destination, int num) {
		

			Node* current = tail->prev;  

			
			for (int i = 0; i < num; i++) {
				Node* nodeToMove = current;    
				current = current->prev;      

				// Remove node from source list
				nodeToMove->prev->next = nodeToMove->next;
				nodeToMove->next->prev = nodeToMove->prev;

				// Insert node at the end of destination list
				nodeToMove->next = destination.tail;
				nodeToMove->prev = destination.tail->prev;
				destination.tail->prev->next = nodeToMove;
				destination.tail->prev = nodeToMove;

				size--;
				destination.size++;
			}
		}


	
	class Iterator {
	public:
		Node* current;

		Iterator() {
			current = NULL;
		}
		Iterator(Node* c) {
			current = c;
		}
		T& operator*() {
			if (current != NULL) {
				return current->data;
			}
		}
		Node* operator->() {
			return current;
		}
		Iterator& operator++() {
			current = current->next;
			return *this;
		}
		Iterator& operator--() {
			current = current->prev;
			return *this;
		}
		Iterator operator++(int) {
			Iterator temp = *this;
			current = current->next;
			return temp;

		}
		Iterator operator--(int) {
			Iterator temp = *this;
			current = current->prev;
			return temp;

		}
		bool operator==(Iterator obj) {

			return(this->current == obj.current);

		}
		bool operator!=(Iterator obj) {

			return(!(this->current == obj.current));

		}

		Iterator Delete(Iterator i) {

			if (i.current->next != NULL) {
				i.current->prev->next = i.current->next;
				i.current->next->prev = i.current->prev;

				Node* temp = i.current->prev;
				delete i.current;

				return Iterator(temp);
			}
			else cout << "List is Empty!" << endl;

		}
		void InsertBefore(Iterator i) {
			i.current->prev = current->prev;
			i.current->next = current;
			current->prev->next = i.current;
			current->prev = i.current;

		}

	};

	Iterator Begin() {
		Iterator obj = head->next;
		return obj;
	}

	Iterator End() {
		Iterator obj = tail;
		return obj;
	}


};


template<class T>
class Stack {
private:

	List<T>* l;
	int size;
public:
	Stack() {
		l = new List<T>;
		size = 0;
	}
	void Push(T data) {

		l->InsertAtHead(data);
		size++;

	}
	bool Pop(T& data) {

		if (!isEmpty()) {
			data = l->getHead();
			l->RemoveAtHead();
			size--;
			return true;
		}
		else {
			return false;
		}
	}
	bool isEmpty() {
		if (size == 0) {
			return true;
		}
		else {
			return false;
		}
	}
	int getSize() {

		return size;
	}
	T GetTop() {
		return l->getHead();
	}
};
