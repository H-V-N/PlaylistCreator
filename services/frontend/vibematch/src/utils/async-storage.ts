const StorageKeys = {
  spotifyAuthResult: 'spotifyAuthResult',
  spotifyState: 'spotifyState'
};
class AsyncStorage {
  get = <T>(key: keyof typeof StorageKeys) =>
    new Promise<T>((res, rej) => {
      try {
        const value = localStorage.getItem(key);
        if (!value) {
          throw new Error('key not found');
        }
        const parsed = JSON.parse(value) as T;
        res(parsed);
      } catch (e) {
        rej(e);
      }
    });

  set = <T>(key: keyof typeof StorageKeys, value: T) =>
    localStorage.setItem(key, JSON.stringify(value));
}

export const StorageInstance = new AsyncStorage();
