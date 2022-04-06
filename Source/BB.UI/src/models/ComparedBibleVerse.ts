import type { ComparedWord } from "./ComparedWord";

export interface ComparedBibleVerse {
    ID: number;
    Book: number;
    Chapter: number;
    Verse: number;
    Text: string;
    ComparedWords: ComparedWord[];
}