import type { Event } from "../Event";

export class SearchWordEvent implements Event {
    public searchWord: string;

    constructor(word: string) {
        this.searchWord = word;
    }
}