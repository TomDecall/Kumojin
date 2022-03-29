import { describe, expect, it, jest } from '@jest/globals';
import { installQuasarPlugin } from '@quasar/quasar-app-extension-testing-unit-jest';
import { mount } from '@vue/test-utils';
import { QBtn, Notify } from 'quasar';
import { NotifyToast } from 'src/services/common/notifyService'

import eventManager from 'src/pages/EventsManager.vue';

installQuasarPlugin()

Notify.create = () => {
  return jest.fn()
}

describe('EventsManager', () => {

  it('the adding dialog showVariable must be true QBtn "Ajouter" is clicked', async() => {
    const wrapper = mount(eventManager);
    const { vm } = wrapper;
    const button = wrapper.findComponent<QBtn>("#addEvent");
    await button.trigger('click');

    await expect(vm.eventDialogCreate.show).toBe(true);
  });

  it('The new Event must be set to id 0 when QBtn "Ajouter" is clicked', async() => {
    const wrapper = mount(eventManager);
    const { vm } = wrapper;
    const button = wrapper.findComponent<QBtn>("#addEvent");
    await button.trigger('click');

    await expect(vm.eventDialogCreate.toUpsert.id).toBe(0);
  });
})
