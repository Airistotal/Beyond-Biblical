import { defineStore } from 'pinia'

export const useBibleStore = defineStore({
  id: 'bible',
  state: () => ({
    mainBible: 1,
    compareBible: 2,
    book: 1,
    chapter: 1
  }),
  actions: {
    setMainBible(bibleID: number) {
      this.mainBible = bibleID;
    },
    setCompareBible(bibleID: number) {
      this.compareBible = bibleID;
    },
    setBibleBook(book: number) {
      this.book = book;
    },
    setBibleChapter(chapter: number) {
      this.chapter = chapter;
    }
  },
  getters: {
    getMainBible: state => {
      return state.mainBible;
    },
    getCompareBible: state => {
      return state.compareBible;
    },
    getBook: state => {
      return state.book;
    },
    getChapter: state => {
      return state.chapter;
    },
    getBibleCompareEndpoint: state => {
      return `${import.meta.env.VITE_APP_API_BASE_URL}/api/BibleComparer?` +
        `mainBible=${state.mainBible}` +
        `&book=${state.book}` +
        `&chapter=${state.chapter}` +
        `&compareBible=${state.compareBible}`;
    },
    getBibleBookChaptersEndpoint: state => {
      return `${import.meta.env.VITE_APP_API_BASE_URL}/api/BibleMeta/GetChaptersForBookInBible?` +
        `bible=${state.mainBible}` +
        `&book=${state.book}`;
    }
  }
})
