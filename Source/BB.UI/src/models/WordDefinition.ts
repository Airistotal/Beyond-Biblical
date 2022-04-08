import type { License } from "./License"
import type { Meaning } from "./Meaning"
import type { Phonetic } from "./Phonetic"

export interface WordDefinition {
    word: string,
    phonetic: string,
    phonetics: Phonetic[],
    license: License,
    sourceUrls: string[],
    origin: string,
    meanings: Meaning[]
}