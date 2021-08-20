export const delay =
  <T>(ms = 100) =>
  (result: T) =>
    new Promise<T>((res) => {
      setTimeout(res, ms, result);
    });
