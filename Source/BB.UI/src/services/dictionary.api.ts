import type { WordDefinition } from '@/models/WordDefinition';
import axios from 'axios';
import type { AxiosResponse } from 'axios';

export class DictionaryApi {
    private url: string = "https://api.dictionaryapi.dev/api/v2/entries/en/";

    getDefinition(word: string): Promise<AxiosResponse<WordDefinition[]>> {
        return axios.get<WordDefinition[]>(this.url + word);
    }
}