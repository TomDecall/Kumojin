import { Tools } from "../services/common/Tools";

export class EventViewModel {
  id: number;
  name: string;
  description: string;
  startDate: Date;
  endDate: Date;

  constructor(id: number, name: string, description: string, startDate: Date, endDate: Date) {
    this.id = id
    this.name = name
    this.description = description
    this.startDate = startDate
    this.endDate = endDate
  }

  get startDateDisplay(): string {
    return Tools.getLocaleDateTime(this.startDate);
  }

  get endDateDisplay(): string {
    return Tools.getLocaleDateTime(this.endDate);
  }

  static getInputModelNameValidator(): Array<Function> {
    return [
      (val: string) => (val.length < 33) || 'Le maximum autorisé est de 32 caractères',
      (val: string) => (val.length != 0) || 'Le nom est obligatoire',
    ]
  }
}

export class EventViewModelMapper {
  static mapToUTCDate(eventViewModel: EventViewModel): EventViewModel {
    eventViewModel.startDate = new Date(eventViewModel.startDate)
    eventViewModel.endDate = new Date(eventViewModel.endDate)
    return eventViewModel
  }

  static mapToUTCDateList(eventViewModelList: Array<EventViewModel>): Array<EventViewModel> {
    return eventViewModelList.map(c => this.mapToUTCDate(c));
  }
}

