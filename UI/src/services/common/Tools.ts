export class Tools {
  static getLocaleDateTime(dateToTransform: Date | string): string {
    if (typeof (dateToTransform) === 'string') {
      return new Date(dateToTransform).toLocaleString()
    }
    return dateToTransform.toLocaleString()
  }
}
