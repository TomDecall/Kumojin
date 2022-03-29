export class dialogDefinition<T> {
  show: boolean;
  title: string
  toUpsert: T

  constructor(show: boolean, title: string, toUpsert: T) {
    this.title = title;
    this.show = show;
    this.toUpsert = toUpsert;
  }
}
