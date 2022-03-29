import { Notify } from 'quasar'

export class NotifyToast {
  static ShowSuccess(message : string) {
    Notify.create({
      icon: 'done',
      message: message,
      color: 'green',
      timeout: 3000
    })
  }

  static ShowFail(message: string) {
    Notify.create({
      icon: 'report_problem',
      message: message,
      color: 'negative',
      timeout: 3000
    })
  }
}

