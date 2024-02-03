class Node {
    constructor(data, next = null) {
        this.data = data;
        this.next = next;
    }
}

class SLL {
    constructor() {
        this.head = null;
    }

    addFront(value) {
        const newNode = new Node(value, this.head);
        this.head = newNode;
        return this.head;
    }
    removeFront() {
        if (this.head === null) {
            return null;
        }

        const newHead = this.head.next;
        this.head = newHead;

        return this.head;
    }
    front() {
        if (this.head === null) {
            return null;
        }

        return this.head.data;
    }
    display() {
        let current = this.head;
        let result = "";

        while (current) {
            result += `${current.data}${current.next ? ', ' : ''}`;
            current = current.next;
        }

        return result;
    }
}
SLL1 = new SLL()
console.log(SLL1.addFront(76) )
console.log(SLL1.addFront(2) )
console.log(SLL1.display() )
console.log(SLL1.addFront(11.41) )
console.log(SLL1.display() )




